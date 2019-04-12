using Boyner.Config.Domain.Repository;
using Boyner.Config.Model;
using System;
using System.Collections.Generic;

namespace Boyner.Config.Service
{
    public class ConfigService
    {
        private readonly ConfigRepository _configRepository;

        public ConfigService(ConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public List<ConfigModel> GetAll()
        {
            var result = _configRepository.FindAll();
            return result;
        }

        public ConfigModel GetById(string id)
        {
            ConfigModel result = _configRepository.GetById(id);
            return result;
        }

        public ConfigModel Create(ConfigModel model)
        {
            var result = _configRepository.Create(model);
            return result;
        }        

        public ConfigModel Update(ConfigModel model)
        {
            var result = _configRepository.Update(model);
            return result;
        }

        public bool Delete(string id)
        {
            var result = _configRepository.Delete(id);
            return result;
        }


    }
}
