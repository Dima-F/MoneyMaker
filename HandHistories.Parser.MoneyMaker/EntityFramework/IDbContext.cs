using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandHistories.SimpleObjects.Entities;

namespace HandHistories.Parser.MoneyMaker.EntityFramework
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : IEntity;
        int SaveChanges();
    }
}
