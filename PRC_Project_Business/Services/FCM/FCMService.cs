using AutoMapper;
using FirebaseAdmin.Messaging;
using PRC_Project.Data.Models;
using PRC_Project.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRC_Project_Business.Services.FCM
{
    public class FCMService : IFCMService
    {
        private readonly IUnitOfWork _uow;
        private IMapper _mapper;
        public FCMService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> CheckDevice(string username, string deviceId)
        {
            UserDevice device = await _uow.UserDeviceRepository.GetFirst(filter: el => el.DeviceId == deviceId && el.Username == username);

            if (device == null)
            {
                UserDevice newDevice = new UserDevice()
                {
                    Username = username,
                    DeviceId = deviceId
                };
                _uow.UserDeviceRepository.Add(newDevice);
            }
            else
            {
                device.DeviceId = deviceId;
                _uow.UserDeviceRepository.Update(device);
            }
            return await _uow.SaveAsync() > 0;
        }

        public async Task SendMessage(string username, string title, string body)
        {
            IEnumerable<UserDevice> device = await _uow.UserDeviceRepository.Get(filter: el => el.Username == username);
            List<UserDevice> list = new List<UserDevice>(device);
            List<Message> messages = new List<Message>();
            if (list.Count > 0)
            {
                list.ForEach(device =>
                {
                    messages.Add(new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = title,
                            Body = body
                        },
                        Token = device.DeviceId
                    });
                });
                await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
            }
        }
    }
}
