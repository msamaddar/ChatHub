//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestChat.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.ChatPosts = new HashSet<ChatPost>();
            this.ChatRoomUsers = new HashSet<ChatRoomUser>();
        }
    
        public long UserID { get; set; }
        public string ConnectionID { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> ConnectedAt { get; set; }
        public Nullable<System.DateTime> DisconnectedAt { get; set; }
    
        public virtual ICollection<ChatPost> ChatPosts { get; set; }
        public virtual ICollection<ChatRoomUser> ChatRoomUsers { get; set; }
    }
}
