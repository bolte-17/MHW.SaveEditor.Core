using MHW.SaveEditor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace MHW.SaveEditor.Test
{
  public enum TestSave
  {
    NewGame,
    PostXeno,
    Endgame1,
    Endgame2
  }

  public class TestSavesFixture
  {
    public IDictionary<TestSave, SaveFile> TestSaves = (new[] {
      (TestSave.NewGame, "HR1"),
      (TestSave.PostXeno, "HR16"),
      (TestSave.Endgame1, "HR100"),
      (TestSave.Endgame2, "HR116"),
    }).ToDictionary(x => x.Item1, x => {
      var (key, folderName) = x;
      return new SaveFile(File.ReadAllBytes($"TestSaves/{folderName}/SAVEDATA1000"));
    });
  }

  [CollectionDefinition("Test Saves")]
  public class TestSavesCollection : ICollectionFixture<TestSavesFixture> { }

  public class AllTestSavesGenerator : IEnumerable<object[]>
  {
    private readonly IEnumerable<object[]> allSaves = Enum.GetValues(typeof(TestSave)).Cast<TestSave>().Select(ts => new object[] { ts });
    public IEnumerator<object[]> GetEnumerator() => allSaves.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => allSaves.GetEnumerator();
  }
}
