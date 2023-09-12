using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.Repository
{
    public static class RealmService
    {
        public static Realm GetRealm() => Realm.GetInstance();
    }
}
