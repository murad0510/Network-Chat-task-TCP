using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Network_Chat_task_TCP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Network_Chat_task_TCP.Helpers
{
    public class FileHelper
    {
        public static void WriteUser(User users)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter("Users.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, users);
                }
            }
        }

        public static List<User> ReadUser()
        {
            List<User> users = null;
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader("Users.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    users = serializer.Deserialize<List<User>>(jr);
                    
                }
            }
            return users;
        }
    }
}
