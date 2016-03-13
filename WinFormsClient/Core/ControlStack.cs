using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsClient
{
	public class ControlStack
	{
		public event EventHandler OnUpdated;

		private readonly Stack<Control> mStack = new Stack<Control>();
		public void Push(Control c)
		{
			mStack.Push(c);
			if (OnUpdated != null)
				OnUpdated(this, new EventArgs());
		}

		public void Pop()
		{
			mStack.Pop();
			if (OnUpdated != null)
				OnUpdated(this, new EventArgs());
		}

		public Control Top
		{
			get
			{
				if (mStack.Count == 0)
					return null;
				return mStack.Peek();
			}
		}
	}
}
