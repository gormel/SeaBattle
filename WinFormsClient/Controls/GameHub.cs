using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Packeges;
using Server;
using WinFormsClient.Controls;

namespace WinFormsClient
{
	public partial class GameHub : UserControl
	{
		private class ListBoxItem
		{
			public Guid ID { get; set; }
			public string Name { get; set; }
			public int UserCount { get; set; }

			public override string ToString()
			{
				return string.Format("{0} ({1})", Name, UserCount);
			}
		}
		private readonly MainForm mMainForm;

		public GameHub(MainForm mainForm)
		{
			this.mMainForm = mainForm;
			InitializeComponent();
		}

		private async void GameHub_Load(object sender, EventArgs e)
		{
			mMainForm.Resolver.AddHandler<RoomList>(RoomListRecived);
			mMainForm.Resolver.AddHandler<RoomInfoPackage>(RoomInfoRecived);
			mMainForm.Resolver.AddHandler<RoomCreatePackage>(RoomCreateRecived);
			mMainForm.Resolver.AddHandler<JoinRoom>(JoinRoomRecived);
			await RefreshRooms();
		}

		private void JoinRoomRecived(JoinRoom joinRoom)
		{
			if (!joinRoom.Result)
				return;
			mMainForm.ControlStack.Push(new RoomControl(mMainForm));
		}

		private async void RoomCreateRecived(RoomCreatePackage roomCreatePackage)
		{
			await RefreshRooms();
			await mMainForm.Talker.Send(mMainForm.Me, new JoinRoom { ID = roomCreatePackage.ID });
		}

		private async void RoomListRecived(RoomList list)
		{
			mRoomsListBox.Items.Clear();
			foreach (var room in list.Rooms)
			{
				await mMainForm.Talker.Send(mMainForm.Me, new RoomInfoPackage
				{
					ID = room
				});
			}
		}

		private async Task RefreshRooms()
		{
			 await mMainForm.Talker.Send(mMainForm.Me, new RoomList());
		}

		private void RoomInfoRecived(RoomInfoPackage info)
		{
			mRoomsListBox.Items.Add(new ListBoxItem { ID = info.ID, Name = info.Name, UserCount = info.UserCount });
		}

		private async void mRefreshButton_Click(object sender, EventArgs e)
		{
			await RefreshRooms();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			 await mMainForm.Talker.Send(mMainForm.Me, new RoomCreatePackage {Name = mRoomName.Text});
		}

		private async void mJoinButton_Click(object sender, EventArgs e)
		{
			await mMainForm.Talker.Send(mMainForm.Me, new JoinRoom { ID = ((ListBoxItem)mRoomsListBox.SelectedItem).ID });
		}
	}
}
