﻿using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repproject.Repositories;
using Repproject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repproject.Repositories.Repositories
{
    public class EventRepository : IEventRepository
    {
        readonly IContext _context;

        public EventRepository(IContext context)
        {
            _context = context;
        }
       
        public async Task<Event> AddAsync(int calenderId, string eventType, string hebrewDate, int userId, string eventYear, bool oneTimeEvent)
        {
            var newEvent = new Event
            { 
                CalenderId = calenderId,
                EventType = eventType,
                HebrewDate = hebrewDate,
                UserId = userId,
                } ;
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task DeleteAsync(int id)
        {
            var eevent = await GetByIdAsync(id);
            _context.Events.Remove(eevent);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

      
        //public async Task<int> GetByTzAsync(string tz)
        //{
        //    var parentsList = await GetAllAsync();
        //    return parentsList.Find(u => u.Tz == tz).Id;
       
        //}
        public async Task<Event> UpdateAsync(Event eevent)
        {
            var updatedEvent = _context.Events.Update(eevent);
            await _context.SaveChangesAsync();
            return updatedEvent.Entity;
        }

        
    }
}
