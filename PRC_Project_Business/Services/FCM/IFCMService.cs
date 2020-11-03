using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services.FCM
{
    public interface IFCMService
    {
        public Task<bool> CheckDevice(string username, string deviceId);

        public Task SendMessage(string username, string title, string body);
    }
}
