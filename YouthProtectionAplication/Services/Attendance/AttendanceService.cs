using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Services.Attendance
{
    public class AttendanceService
    {
        private readonly Request _request;

        private string _token;

        private const string apiUrlBase = "http://localhost:5189/Attendance";

        public AttendanceService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<long> StartAttendance(long publicationId)
        {
            string urlComplementar = string.Format("/start/" + "{0}", publicationId);
         var result = await _request.PostAsync<Models.ChatUser>(apiUrlBase + urlComplementar , null, _token);

            return result.ChatId;
        }

        public async Task CompleteAttendance(long ChatId)
        {
            string urlComplementar = string.Format("/complete/" + "{0}", ChatId);
            var result = await _request.PostAsync<Models.ChatUser>(apiUrlBase + urlComplementar, null, _token);

            return;
        }

    }
}
