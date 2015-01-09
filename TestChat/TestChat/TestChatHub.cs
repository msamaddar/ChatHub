using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TestChat.Model;
using TestChat.Business;
using log4net;
using TestChat.DataView;

namespace TestChat
{
    [HubName("chat")]
    public class TestChatHub : Hub
    {
        ILog logger = LogManager.GetLogger(typeof(TestChatHub));
        public void SendMessage(string message)
        {
            Clients.All.newMessage(FormattedMessage(message));
        }
        public void GetRoomName()
        {
            try
            {
                ChatRoomService chatRoomService = new ChatRoomService();
                var roomName = chatRoomService.GetRoomName();

                logger.Info(string.Format("Room Name: {0}", roomName));

                if (!string.IsNullOrEmpty(roomName))
                    Clients.Caller.roomName(roomName);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
        public void JoinRoom(string roomName, string userName)
        {
            Groups.Add(Context.ConnectionId, roomName);

            UserService userService = new UserService();
            userService.AddUser(userName, Context.ConnectionId);

            ChatRoomUserService chatRoomService = new ChatRoomUserService();
            chatRoomService.AddUserToChatRoom(userName, roomName);

            Clients.Caller.recentPosts(new ChatPostService().GetRecentPosts());
            Clients.All.chatRoomUsers(new ChatRoomUserService().GetLoggedUsers(roomName));
        }
        public void LeaveRoom(string roomName)
        {
            Groups.Remove(Context.ConnectionId, roomName);

            new ChatRoomUserService().RemoveUserFromChatRoom(Context.ConnectionId, roomName);
            new UserService().RemoveUser(Context.ConnectionId);
        }
        public void SendMessageToRoom(string roomName, string message)
        {
            Clients.Group(roomName).newMessage(FormattedMessage(message));
        }
        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }
        private string FormattedMessage(string message)
        {
            ChatPostService chatPostService = new ChatPostService();
            chatPostService.SavePost(Context.ConnectionId, message);

            return string.Format("{0}: {1}", new UserService().GetUserNameByConnectionID(Context.ConnectionId), message);
        }
        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }
    }
}