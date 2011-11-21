﻿using LessonsLearned.DomainModel;
using LessonsLearned.DomainModel.Common;
using Microsoft.Practices.ServiceLocation;


namespace LessonsLearned.Application.Controller
{
    public class ApplicationController : IApplicationController
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IEventPublisher _eventPublisher;
        private IHost _host;

        public ApplicationController(IServiceLocator serviceLocator, IEventPublisher eventPublisher)
        {
            _serviceLocator = serviceLocator;
            _eventPublisher = eventPublisher;
        }

        public void Execute<T>(T commandData)
        {
            var commands = _serviceLocator.GetAllInstances<ICommand<T>>();
            foreach (var command in commands)
            {
                command.Execute(commandData);
            }
        }

        public void Raise<T>(T eventData)
        {
            _eventPublisher.Publish(eventData);
        }

        public void ShowInHost(IView view)
        {
            _host.ShowInHost(view);
        }

        public void SetHost(IHost host)
        {
            _host = host;
        }
    }
}
