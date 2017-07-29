using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;


namespace TroopDetailPlugin
{
	internal class TroopDetail
	{
		internal PlatformTexture BackgroundTexture;

		internal Point BackgroundSize;

		internal FreeText TroopNameText;

		internal Rectangle PortraitClient;

		internal Troop ShowingTroop;

		internal List<LabelText> LabelTexts;

		internal FreeRichText OtherPersonText;

		internal Rectangle OtherPersonClient;

		internal FreeRichText CombatMethodText;

		internal Rectangle CombatMethodClient;

		internal FreeRichText StuntText;

		internal Rectangle StuntClient;

		internal FreeRichText InfluenceText;

		internal Rectangle InfluenceClient;
        
		private bool isShowing;

		private Point DisplayOffset;

		private Rectangle BackgroundDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
				return rectangle;
			}
		}

		private Rectangle CombatMethodDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.CombatMethodText.DisplayOffset.X, this.CombatMethodText.DisplayOffset.Y, this.CombatMethodText.ClientWidth, this.CombatMethodText.ClientHeight);
				return rectangle;
			}
		}

		private Rectangle InfluenceDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.InfluenceText.DisplayOffset.X, this.InfluenceText.DisplayOffset.Y, this.InfluenceText.ClientWidth, this.InfluenceText.ClientHeight);
				return rectangle;
			}
		}

		public bool IsShowing
		{
			get
			{
				bool flag = this.isShowing;
				return flag;
			}
			set
			{
				this.isShowing = value;
				bool kind = !value;
				if (kind)
				{
                    kind = Session.MainGame.mainGameScreen.PopUndoneWork().Kind == UndoneWorkKind.SubDialog;
					if (kind)
					{
						Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
						Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
						Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
						this.OtherPersonText.Clear();
						this.CombatMethodText.Clear();
						this.StuntText.Clear();
						this.InfluenceText.Clear();
					}
					else
					{
						throw new Exception("The UndoneWork is not a SubDialog.");
					}
				}
				else
				{
					Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.SubDialog, DialogKind.TroopDetail));
					Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
					Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
					Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
				}
			}
		}

		private Rectangle OtherPersonDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.OtherPersonText.DisplayOffset.X, this.OtherPersonText.DisplayOffset.Y, this.OtherPersonText.ClientWidth, this.OtherPersonText.ClientHeight);
				return rectangle;
			}
		}

		private Rectangle PortraitDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.PortraitClient.X + this.DisplayOffset.X, this.PortraitClient.Y + this.DisplayOffset.Y, this.PortraitClient.Width, this.PortraitClient.Height);
				return rectangle;
			}
		}

		private Rectangle StuntDisplayPosition
		{
			get
			{
				Rectangle rectangle = new Rectangle(this.StuntText.DisplayOffset.X, this.StuntText.DisplayOffset.Y, this.StuntText.ClientWidth, this.StuntText.ClientHeight);
				return rectangle;
			}
		}

		public TroopDetail()
		{
			this.LabelTexts = new List<LabelText>();
			this.OtherPersonText = new FreeRichText();
			this.CombatMethodText = new FreeRichText();
			this.StuntText = new FreeRichText();
			this.InfluenceText = new FreeRichText();
		}

		internal void Draw()
		{
			bool showingTroop = this.ShowingTroop != null;
			if (showingTroop)
			{
				Rectangle? nullable = null;
				CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
				nullable = null;

                CacheManager.DrawZhsanAvatar(this.ShowingTroop.Leader, "s", this.PortraitDisplayPosition, Color.White, 0.199f);

				this.TroopNameText.Draw(0.1999f);
				List<LabelText>.Enumerator enumerator = this.LabelTexts.GetEnumerator();
				try
				{
					while (true)
					{
						showingTroop = enumerator.MoveNext();
						if (!showingTroop)
						{
							break;
						}
						LabelText current = enumerator.Current;
						current.Label.Draw(0.1999f);
						current.Text.Draw(0.1999f);
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				this.OtherPersonText.Draw(0.1999f);
				this.CombatMethodText.Draw(0.1999f);
				this.StuntText.Draw(0.1999f);
				this.InfluenceText.Draw(0.1999f);
			}
		}

		internal void Initialize()
		{
			
		}

		private void screen_OnMouseLeftDown(Point position)
		{
			bool currentPageIndex = !StaticMethods.PointInRectangle(position, this.OtherPersonDisplayPosition);
			if (currentPageIndex)
			{
				currentPageIndex = !StaticMethods.PointInRectangle(position, this.CombatMethodDisplayPosition);
				if (currentPageIndex)
				{
					currentPageIndex = !StaticMethods.PointInRectangle(position, this.StuntDisplayPosition);
					if (currentPageIndex)
					{
						currentPageIndex = !StaticMethods.PointInRectangle(position, this.InfluenceDisplayPosition);
						if (!currentPageIndex)
						{
							currentPageIndex = this.InfluenceText.CurrentPageIndex >= this.InfluenceText.PageCount - 1;
							if (currentPageIndex)
							{
								currentPageIndex = this.InfluenceText.CurrentPageIndex != this.InfluenceText.PageCount - 1;
								if (!currentPageIndex)
								{
									this.InfluenceText.FirstPage();
								}
							}
							else
							{
								this.InfluenceText.NextPage();
							}
						}
					}
					else
					{
						currentPageIndex = this.StuntText.CurrentPageIndex >= this.StuntText.PageCount - 1;
						if (currentPageIndex)
						{
							currentPageIndex = this.StuntText.CurrentPageIndex != this.StuntText.PageCount - 1;
							if (!currentPageIndex)
							{
								this.StuntText.FirstPage();
							}
						}
						else
						{
							this.StuntText.NextPage();
						}
					}
				}
				else
				{
					currentPageIndex = this.CombatMethodText.CurrentPageIndex >= this.CombatMethodText.PageCount - 1;
					if (currentPageIndex)
					{
						currentPageIndex = this.CombatMethodText.CurrentPageIndex != this.CombatMethodText.PageCount - 1;
						if (!currentPageIndex)
						{
							this.CombatMethodText.FirstPage();
						}
					}
					else
					{
						this.CombatMethodText.NextPage();
					}
				}
			}
			else
			{
				currentPageIndex = this.OtherPersonText.CurrentPageIndex >= this.OtherPersonText.PageCount - 1;
				if (currentPageIndex)
				{
					currentPageIndex = this.OtherPersonText.CurrentPageIndex != this.OtherPersonText.PageCount - 1;
					if (!currentPageIndex)
					{
						this.OtherPersonText.FirstPage();
					}
				}
				else
				{
					this.OtherPersonText.NextPage();
				}
			}
		}

		private void screen_OnMouseMove(Point position, bool leftDown)
		{
		}

		private void screen_OnMouseRightUp(Point position)
		{
			this.IsShowing = false;
		}

		internal void SetPosition(ShowPosition showPosition)
		{
			Rectangle rectangle = new Rectangle(0, 0, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y);
			Rectangle centerRectangle = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
			ShowPosition showPosition1 = showPosition;
			switch (showPosition1)
			{
				case ShowPosition.Center:
				{
					centerRectangle = StaticMethods.GetCenterRectangle(rectangle, centerRectangle);
					break;
				}
				case ShowPosition.Top:
				{
					centerRectangle = StaticMethods.GetTopRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.Left:
				{
					centerRectangle = StaticMethods.GetLeftRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.Right:
				{
					centerRectangle = StaticMethods.GetRightRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.Bottom:
				{
					centerRectangle = StaticMethods.GetBottomRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.TopLeft:
				{
					centerRectangle = StaticMethods.GetTopLeftRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.TopRight:
				{
					centerRectangle = StaticMethods.GetTopRightRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.BottomLeft:
				{
					centerRectangle = StaticMethods.GetBottomLeftRectangle(rectangle, centerRectangle);
					break;
				}
                case ShowPosition.BottomRight:
				{
					centerRectangle = StaticMethods.GetBottomRightRectangle(rectangle, centerRectangle);
					break;
				}
			}
			this.DisplayOffset = new Point(centerRectangle.X, centerRectangle.Y);
			this.TroopNameText.DisplayOffset = this.DisplayOffset;
			List<LabelText>.Enumerator enumerator = this.LabelTexts.GetEnumerator();
			try
			{
				while (true)
				{
					bool flag = enumerator.MoveNext();
					if (!flag)
					{
						break;
					}
					LabelText current = enumerator.Current;
					current.Label.DisplayOffset = this.DisplayOffset;
					current.Text.DisplayOffset = this.DisplayOffset;
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			this.OtherPersonText.DisplayOffset = new Point(this.DisplayOffset.X + this.OtherPersonClient.X, this.DisplayOffset.Y + this.OtherPersonClient.Y);
			this.CombatMethodText.DisplayOffset = new Point(this.DisplayOffset.X + this.CombatMethodClient.X, this.DisplayOffset.Y + this.CombatMethodClient.Y);
			this.StuntText.DisplayOffset = new Point(this.DisplayOffset.X + this.StuntClient.X, this.DisplayOffset.Y + this.StuntClient.Y);
			this.InfluenceText.DisplayOffset = new Point(this.DisplayOffset.X + this.InfluenceClient.X, this.DisplayOffset.Y + this.InfluenceClient.Y);
		}

		internal void SetTroop(Troop troop)
		{
			bool leader;
			this.ShowingTroop = troop;
			this.TroopNameText.Text = troop.DisplayName;
			List<LabelText>.Enumerator enumerator = this.LabelTexts.GetEnumerator();
			try
			{
				while (true)
				{
					leader = enumerator.MoveNext();
					if (!leader)
					{
						break;
					}
					LabelText current = enumerator.Current;
					current.Text.Text = StaticMethods.GetPropertyValue(troop, current.PropertyName).ToString();
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			this.OtherPersonText.AddText("其他人物", this.OtherPersonText.TitleColor);
			this.OtherPersonText.AddNewLine();
			int personCount = troop.PersonCount - 1;
			this.OtherPersonText.AddText(string.Concat(personCount.ToString(), "人"), this.OtherPersonText.SubTitleColor);
			this.OtherPersonText.AddNewLine();
			IEnumerator enumerator1 = troop.Persons.GetEnumerator();
			try
			{
				while (true)
				{
					leader = enumerator1.MoveNext();
					if (!leader)
					{
						break;
					}
					Person person = (Person)enumerator1.Current;
					leader = person != troop.Leader;
					if (leader)
					{
						this.OtherPersonText.AddText(person.Name);
						this.OtherPersonText.AddNewLine();
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator1 as IDisposable;
				leader = disposable == null;
				if (!leader)
				{
					disposable.Dispose();
				}
			}
			this.OtherPersonText.ResortTexts();
			this.CombatMethodText.AddText("部队战法", this.CombatMethodText.TitleColor);
			this.CombatMethodText.AddNewLine();
			personCount = troop.CombatMethods.Count;
			this.CombatMethodText.AddText(string.Concat(personCount.ToString(), "种"), this.CombatMethodText.SubTitleColor);
			this.CombatMethodText.AddNewLine();
			Dictionary<int, CombatMethod>.ValueCollection.Enumerator enumerator2 = troop.CombatMethods.CombatMethods.Values.GetEnumerator();
			try
			{
				while (true)
				{
					leader = enumerator2.MoveNext();
					if (!leader)
					{
						break;
					}
					CombatMethod combatMethod = enumerator2.Current;
					this.CombatMethodText.AddText(combatMethod.Name, this.CombatMethodText.SubTitleColor2);
					personCount = combatMethod.Combativity - troop.DecrementOfCombatMethodCombativityConsuming;
					this.CombatMethodText.AddText(string.Concat(" 战意消耗", personCount.ToString()), this.CombatMethodText.SubTitleColor3);
					this.CombatMethodText.AddNewLine();
				}
			}
			finally
			{
				enumerator2.Dispose();
			}
			this.CombatMethodText.ResortTexts();
			this.StuntText.AddText("部队特技", this.StuntText.TitleColor);
			this.StuntText.AddNewLine();
			personCount = troop.Stunts.Count;
			this.StuntText.AddText(string.Concat(personCount.ToString(), "种"), this.StuntText.SubTitleColor);
			this.StuntText.AddNewLine();
			Dictionary<int, Stunt>.ValueCollection.Enumerator enumerator3 = troop.Stunts.Stunts.Values.GetEnumerator();
			try
			{
				while (true)
				{
					leader = enumerator3.MoveNext();
					if (!leader)
					{
						break;
					}
					Stunt stunt = enumerator3.Current;
					this.StuntText.AddText(stunt.Name, this.StuntText.SubTitleColor2);
					personCount = stunt.Combativity;
					this.StuntText.AddText(string.Concat(" 战意消耗", personCount.ToString()), this.StuntText.SubTitleColor3);
					this.StuntText.AddNewLine();
				}
			}
			finally
			{
				enumerator3.Dispose();
			}
			this.StuntText.ResortTexts();
			this.InfluenceText.AddText("部队特性", this.InfluenceText.TitleColor);
			this.InfluenceText.AddNewLine();
			this.InfluenceText.AddText(this.ShowingTroop.Army.Kind.Name, this.InfluenceText.SubTitleColor);
			this.InfluenceText.AddNewLine();
			Dictionary<int, Influence>.ValueCollection.Enumerator enumerator4 = this.ShowingTroop.Army.Kind.Influences.Influences.Values.GetEnumerator();
			try
			{
				while (true)
				{
					leader = enumerator4.MoveNext();
					if (!leader)
					{
						break;
					}
					Influence influence = enumerator4.Current;
					this.InfluenceText.AddText(influence.Name, this.InfluenceText.SubTitleColor2);
					this.InfluenceText.AddText(influence.Description, this.InfluenceText.SubTitleColor3);
					this.InfluenceText.AddNewLine();
				}
			}
			finally
			{
				enumerator4.Dispose();
			}
			this.InfluenceText.ResortTexts();
		}
	}
}
