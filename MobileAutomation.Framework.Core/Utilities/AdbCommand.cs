using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileAutomation.Framework.Core.Utilities {
    public class AdbCommand {

        [JsonProperty("command")]
        private string Command { get; set; }

        [JsonProperty("args")]
        private List<string> Args { get; set; }
        public AdbCommand(string command) {
            this.Command = command;
            this.Args = new List<string>();
        }
        public AdbCommand(string command, params string[] args) {
            this.Command = command;
            this.Args = new List<string>(args);
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}