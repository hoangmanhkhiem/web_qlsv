
using Ical.Net;
using System.Net.Http;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
using qlsv.Models;

namespace qlsv.Helpers;

public class CalendarHelper
{
    // Variables

    // Constructors

    // Convert ICS to Model
    public List<qlsv.Models.CalendarEvent> ConvertIcsToCalendarEvents(string icsFilePath)
    {
        // Đọc tệp ICS
        var calendar = Calendar.Load(File.ReadAllText(icsFilePath));

        // Lấy tất cả các sự kiện trong tệp ICS
        var events = calendar.Events;

        // Tạo danh sách các sự kiện cho FullCalendar
        var calendarEvents = new List<qlsv.Models.CalendarEvent>();

        foreach (var icalEvent in events)
        {
            var calendarEvent = new qlsv.Models.CalendarEvent
            {
                Id = icalEvent.Uid,  // Uid trong ICS sẽ làm Id cho event
                Title = icalEvent.Summary,  // Summary trong ICS tương ứng với Title
                Start = icalEvent.Start.AsSystemLocal,  // Thời gian bắt đầu sự kiện
                End = icalEvent.End?.AsSystemLocal,  // Thời gian kết thúc (nếu có)
                AllDay = icalEvent.IsAllDay,  // Kiểm tra sự kiện có cả ngày hay không
                Description = icalEvent.Description,  // Mô tả sự kiện
                Location = icalEvent.Location,  // Địa điểm
                RecurrenceRule = icalEvent.RecurrenceRules?.FirstOrDefault()?.ToString() // Quy tắc lặp lại (nếu có)
            };

            // TODO - Thêm các thuộc tính khác như BackgroundColor, BorderColor, TextColor, Url nếu cần.

            calendarEvents.Add(calendarEvent);
        }

        return calendarEvents;
    }

    public async Task<List<qlsv.Models.CalendarEvent>> ConvertIcsToCalendarEventsFromLink(string icsUrl)
    {
        // Khởi tạo HttpClient để tải nội dung từ URL
        using (HttpClient client = new HttpClient())
        {
            // Tải nội dung từ URL
            var icsContent = await client.GetStringAsync(icsUrl);

            // Đọc nội dung ICS
            var calendar = Calendar.Load(icsContent);

            // Lấy tất cả các sự kiện trong tệp ICS
            var events = calendar.Events;

            // Tạo danh sách các sự kiện cho FullCalendar
            var calendarEvents = new List<qlsv.Models.CalendarEvent>();

            foreach (var icalEvent in events)
            {
                var calendarEvent = new qlsv.Models.CalendarEvent
                {
                    Id = icalEvent.Uid,  // Uid trong ICS sẽ làm Id cho event
                    Title = icalEvent.Summary,  // Summary trong ICS tương ứng với Title
                    Start = icalEvent.Start.AsSystemLocal,  // Thời gian bắt đầu sự kiện
                    End = icalEvent.End?.AsSystemLocal,  // Thời gian kết thúc (nếu có)
                    AllDay = icalEvent.IsAllDay,  // Kiểm tra sự kiện có cả ngày hay không
                    Description = icalEvent.Description,  // Mô tả sự kiện
                    Location = icalEvent.Location,  // Địa điểm
                    RecurrenceRule = icalEvent.RecurrenceRules?.FirstOrDefault()?.ToString() // Quy tắc lặp lại (nếu có)
                };

                // TODO - Thêm các thuộc tính khác như BackgroundColor, BorderColor, TextColor, Url nếu cần.

                calendarEvents.Add(calendarEvent);
            }

            return calendarEvents;
        }
    }
    
    // Model to Object JSON serialization
    public string SerializeCalendarEvents(List<qlsv.Models.CalendarEvent> calendarEvents)
    {
        return System.Text.Json.JsonSerializer.Serialize(calendarEvents);
    }
}