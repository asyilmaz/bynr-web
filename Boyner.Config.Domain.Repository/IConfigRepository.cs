using System;
using System.Collections.Generic;
using System.Text;
using Boyner.Config.Model;

namespace Boyner.Config.Domain.Repository
{
    public interface IConfigRepository
    {
        ConfigModel GetById(string id);
        List<ConfigModel> FindAll();
        ConfigModel Create(ConfigModel model);
        ConfigModel Update(ConfigModel entity);
        bool Delete(string id);
    }
}
