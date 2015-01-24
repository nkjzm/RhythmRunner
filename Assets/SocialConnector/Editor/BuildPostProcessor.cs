using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public static class XCodePostProcess
{
		[PostProcessBuild]
		public static void OnPostProcessBuild(BuildTarget target, string xcodeProjectPath)
		{
				if(target != BuildTarget.iPhone) return;

				CopyImages(xcodeProjectPath);
		}
		private static void CopyImages(string xcodeProjectPath)
		{
				var destDirName = Path.Combine(xcodeProjectPath, "Unity-iPhone/Images.xcassets/");
				var sourceDirNames = Directory.GetDirectories(Application.dataPath, "*.imageset", SearchOption.AllDirectories);
				foreach(var sourceDirName in sourceDirNames)
				{
						var dirName = Path.GetFileName(sourceDirName);
						CopyDirectory(sourceDirName, Path.Combine(destDirName, dirName));
				}
		}

		// copy from http://dobon.net/vb/dotnet/file/copyfolder.html
		// unityの.metaファイルが含まれないようにちょっとだけ改造。
		public static void CopyDirectory(string sourceDirName, string destDirName)
		{
				//コピー先のディレクトリがないときは作る
				if (!System.IO.Directory.Exists(destDirName))
				{
						System.IO.Directory.CreateDirectory(destDirName);
						//属性もコピー
						System.IO.File.SetAttributes(destDirName,
								System.IO.File.GetAttributes(sourceDirName));
				}

				//コピー先のディレクトリ名の末尾に"\"をつける
				if (destDirName[destDirName.Length - 1] !=
						System.IO.Path.DirectorySeparatorChar)
						destDirName = destDirName + System.IO.Path.DirectorySeparatorChar;

				//コピー元のディレクトリにあるファイルをコピー
				string[] files = System.IO.Directory.GetFiles(sourceDirName);
				foreach (string file in files)
				{
						if(file.EndsWith(".meta")) continue; // metaファイルはコピーしない.
						System.IO.File.Copy(file, destDirName + System.IO.Path.GetFileName(file), true);
				}

				//コピー元のディレクトリにあるディレクトリについて、再帰的に呼び出す
				string[] dirs = System.IO.Directory.GetDirectories(sourceDirName);
				foreach (string dir in dirs)
						CopyDirectory(dir, destDirName + System.IO.Path.GetFileName(dir));
		}
}