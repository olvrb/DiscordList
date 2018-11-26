using System;
using System.Collections.Generic;
using Discord.WebSocket;

namespace FallProject.Models {
    public partial class Message {
        public void createOrUpdate(SocketMessage message) {
            this.Content   = message.Content;
            this.Id        = message.Id.ToString();
            this.ChannelId = message.Channel.Id.ToString();
            this.GuildId   = "placeholder";
        }

        public string Id        { get; set; }
        public string Content   { get; set; }
        public string ChannelId { get; set; }
        public string GuildId   { get; set; }
    }
}
