using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment.DataAccess.Repositories;
using AppointmentWebApp.Exceptions;
using AppointmentWebApp.Mappers;
using AppointmentWebApp.Models;
using Microsoft.Extensions.Logging;
using User = Appointment.Entities.User;

namespace AppointmentWebApp.Services
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<Appoitment>> GetAppointmentsAsync(Guid userId);
        Task<Appoitment> GetAppointmentAsync(Guid appointmentId);

        Task<Guid> CreateAppointmentAsync(CreateAppointment appointment);
        Task UpdateAppointmentAsync(Guid userId, Guid appointmentId, UpdateAppointment appointment);
        Task DeleteAppointmentAsync(Guid userId, Guid appointmentId);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IUserRepository userRepository,
            ILogger<AppointmentService> logger)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Appoitment>> GetAppointmentsAsync(Guid userId)
        {
            var user = await EnsureGetUserAsync(userId);
            var appointments = await _appointmentRepository.GetAppointmentsAsync(user.Id);
            var mappedAppointments = AppointmentMapper.Map(appointments);

            return mappedAppointments;
        }

        public async Task<Appoitment> GetAppointmentAsync(Guid appointmentId)
        {
            var appointment = await EnsureGetAppointmentAsync(appointmentId);

            var mappedAppointment = AppointmentMapper.Map(appointment);
            return mappedAppointment;
        }

        public async Task<Guid> CreateAppointmentAsync(CreateAppointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            var user = await EnsureGetUserAsync(appointment.CreatedByUser);
            var originalAppointment = AppointmentMapper.Map(appointment, user.Id);
            ValidateDateRange(originalAppointment);

            await _appointmentRepository.CreateAppointmentAsync(originalAppointment);
            await _appointmentRepository.SaveAsync();
            return originalAppointment.EntityId;
        }

        public async Task UpdateAppointmentAsync(Guid userId, Guid appointmentId, UpdateAppointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            var originalAppointment = await EnsureGetAppointmentAsync(appointmentId);
            var user = await EnsureGetUserAsync(userId);
            ValidateUserAccess(user, originalAppointment);

            ValidateDateRange(originalAppointment);
            UpdateAppointment(originalAppointment, appointment);
            _appointmentRepository.UpdateAppointment(originalAppointment);
            await _appointmentRepository.SaveAsync();
        }

        private void UpdateAppointment(Appointment.Entities.Appointment originalAppointment,
            UpdateAppointment appointment)
        {
            originalAppointment.Name = appointment.AppointmentName;
            originalAppointment.Guests = appointment.Guests;
            originalAppointment.StartDate = appointment.StartDate;
            originalAppointment.EndDate = appointment.EndDate;
        }

        public async Task DeleteAppointmentAsync(Guid userId, Guid appointmentId)
        {
            var originalAppointment = await EnsureGetAppointmentAsync(appointmentId);
            var user = await EnsureGetUserAsync(userId);
            ValidateUserAccess(user, originalAppointment);

            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
            await _appointmentRepository.SaveAsync();
        }

        private async Task<User> EnsureGetUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                _logger.LogWarning("User '{userId}' not found.", userId);
                throw new EntityNotFoundException(userId, $"User '{userId}' not found.");
            }

            return user;
        }

        private async Task<Appointment.Entities.Appointment> EnsureGetAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentAsync(appointmentId);
            if (appointment == null)
            {
                _logger.LogWarning("Appointment '{appointmentId}' not found.", appointmentId);
                throw new EntityNotFoundException(appointmentId, $"Appointment '{appointmentId}' not found.");
            }

            return appointment;
        }

        private void ValidateUserAccess(User user, Appointment.Entities.Appointment appointment)
        {
            if (user.Id != appointment.CreatedById)
            {
                _logger.LogWarning("Forbidden access to appointment '{entityId}'", appointment.EntityId);
                throw new ForbiddenAccessException(
                    $"The user '{user.EntityId}' can't change the appointment '{appointment.EntityId}'.");
            }
        }

        private void ValidateDateRange(Appointment.Entities.Appointment appointment)
        {
            if (appointment.StartDate > appointment.EndDate)
            {
                _logger.LogWarning("Invalid appointment date range '{startDate}' - '{endDate}", appointment.StartDate,
                    appointment.EndDate);
                throw new InvalidDateRangeException("Invalid date range.");
            }
        }
    }
}