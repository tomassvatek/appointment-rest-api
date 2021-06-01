using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment.DataAccess.Exceptions;
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
            var user = await EnsureGetUserAsync(appointment.CreatedByUser);
            var entity = AppointmentMapper.Map(appointment, user.Id);

            var entityId = await _appointmentRepository.CreateAppointmentAsync(entity);
            return entityId;
        }

        public async Task UpdateAppointmentAsync(Guid userId, Guid appointmentId, UpdateAppointment appointment)
        {
            var originalAppointment = await EnsureGetAppointmentAsync(appointmentId);
            var user = await EnsureGetUserAsync(userId);
            ValidateUserAccess(user, originalAppointment);

            var entity = AppointmentMapper.Map(appointment);
            await _appointmentRepository.UpdateAppointmentAsync(appointmentId, entity);
        }

        public async Task DeleteAppointmentAsync(Guid userId, Guid appointmentId)
        {
            var originalAppointment = await EnsureGetAppointmentAsync(appointmentId);
            var user = await EnsureGetUserAsync(userId);
            ValidateUserAccess(user, originalAppointment);
            
            await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
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
                _logger.LogWarning($"Forbidden access to appointment '{appointment.EntityId}'");
                throw new ForbiddenAccessException(
                    $"The user '{user.EntityId}' can't change the appointment '{appointment.EntityId}'.");
            }
        }
        
    }
}