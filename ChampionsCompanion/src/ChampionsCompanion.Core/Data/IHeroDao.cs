using ChampionsCompanion.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsCompanion.Core.Data
{
    public interface IHeroDao
    {
        public List<Hero> getAll();
    }
}
