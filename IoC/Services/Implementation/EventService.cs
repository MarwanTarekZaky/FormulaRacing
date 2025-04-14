using System.Linq.Expressions;
using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace IoC.Services.Implementation;

public class EventService : IEventService
{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
        }

        public async Task AddAsync(EventDTO eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            if (String.IsNullOrEmpty(eventEntity.VideoUrl))
            {
                eventEntity.VideoUrl = Alternatives.Alternatives.EventVideoUrl;
            }
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(EventDTO eventDto)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventDto.Id);
            if (eventEntity == null) return;
            _mapper.Map(eventDto, eventEntity);
            _unitOfWork.Events.Update(eventEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventEntity != null)
            {
               
                _unitOfWork.Events.Remove(eventEntity);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<EventDTO> GetByIdAsync(int eventId)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventEntity == null) return null;

            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<IEnumerable<EventDTO>> GetAllAsync(Expression<Func<Event,bool>>? filter = null)
        {
            var events = await _unitOfWork.Events.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }
        
}