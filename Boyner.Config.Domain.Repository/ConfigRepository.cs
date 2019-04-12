using Boyner.Config.Infrastructure;
using Boyner.Config.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boyner.Config.Domain.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        public readonly MongoDbContext _dbContext;
        public ConfigRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ConfigModel GetById(string id)
        {
            FilterDefinition<Config> filterDef = Builders<Config>.Filter.Eq("Id", id);
            Config entity = _dbContext.Configs.FindSync<Config>(filterDef).SingleOrDefault();

            ConfigModel model = new ConfigModel()
            {
                Id = entity.Id,
                ApplicationName = entity.ApplicationName,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Type = entity.Type,
                Value = entity.Value
            };

            return model;
        }

        public List<ConfigModel> FindAll()
        {
            List<Config> configs = _dbContext.Configs.AsQueryable().ToList();

            List<ConfigModel> resultList = new List<ConfigModel>();
            //AutoMap
            if (configs.Count > 0)
            {
                for (int i = 0; i < configs.Count; i++)
                {
                    resultList.Add(new ConfigModel()
                    {
                        Id = configs[i].Id,
                        ApplicationName = configs[i].ApplicationName,
                        IsActive = configs[i].IsActive,
                        Name = configs[i].Name,
                        Type = configs[i].Type,
                        Value = configs[i].Value
                    });
                }
            }

            return resultList;
        }

        public ConfigModel Create(ConfigModel model)
        {
            //AutoMap

            Config entity = new Config()
            {
                Id = model.Id,
                ApplicationName = model.ApplicationName,
                IsActive = true,
                Name = model.Name,
                Type = model.Type,
                Value = model.Value
            };

            _dbContext.Configs.InsertOne(entity);

            model = new ConfigModel()
            {
                Id = entity.Id,
                ApplicationName = entity.ApplicationName,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Type = entity.Type,
                Value = entity.Value
            };

            return model;
        }

        public ConfigModel Update(ConfigModel model)
        {
            //AutoMap

            Config entity = new Config()
            {
                Id = model.Id,
                ApplicationName = model.ApplicationName,
                IsActive = model.IsActive,
                Name = model.Name,
                Type = model.Type,
                Value = model.Value
            };

            FilterDefinition<Config> filterDef = Builders<Config>.Filter.Eq("Id", entity.Id);
            _dbContext.Configs.ReplaceOne(filterDef, entity); //Get Result

            model = new ConfigModel()
            {
                Id = entity.Id,
                ApplicationName = entity.ApplicationName,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Type = entity.Type,
                Value = entity.Value
            };

            return model;
        }

        public bool Delete(string id)
        {
            var result = _dbContext.Configs.DeleteOne(x => x.Id == id);
            return result.IsAcknowledged;
        }
    }
}
