using Sandbox;
using System;
namespace Editor;

public static class MotivationManager
{
	static RealTimeUntil Cooldown;

	static MotivationManager()
	{
		Game.SetRandomSeed( DateTime.Now.Second );
		Cooldown = 5;
	}

	private static bool _hasMotivation => NoticeManager.All.FirstOrDefault( x => x is MotivationNotice ) != null;

	[EditorEvent.FrameAttribute]
	public static void Frame()
	{
		
		if ( NoticeManager.All.Any() && !_hasMotivation )
		{
			foreach ( var notice in NoticeManager.All.ToList() )
			{
				if ( notice.IsValid() && notice.GetType().ToString() == "Editor.CodeCompileNotice" && notice is NoticeWidget widget && widget.BorderColor == Theme.Red )
				{
					EditorUtility.PlayAssetSound( SoundFile.Load( "sounds/baka.wav") );
					_ = new MotivationNotice();
				}
			}
		}
		
		/*if ( !Cooldown )
		{
			return;
		}

		_ = new MotivationNotice();

		const int MIN_MINUTES = 15;
		const int MAX_MINUTES = 30;

		Cooldown = 60 * Game.Random.Next( MIN_MINUTES, MAX_MINUTES );*/
	}
}
