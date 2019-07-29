using MHW.SaveEditor.Core;
using System.Collections.Generic;
using Xunit;

namespace MHW.SaveEditor.Test
{
  [Collection("Test Saves")]
  public class FileLoadingTests
  {
    private readonly IDictionary<TestSave, SaveFile> testFiles;
    public FileLoadingTests(TestSavesFixture fixture) => testFiles = fixture.TestSaves;

    [Theory]
    [ClassData(typeof(AllTestSavesGenerator))]
    public void FileLoads(TestSave testSave) => Assert.True(testFiles[testSave].IsDecrypted);

    [Theory]
    [InlineData(TestSave.NewGame, 76561198049553170UL)]
    [InlineData(TestSave.PostXeno, 0UL)]
    [InlineData(TestSave.Endgame1, 0UL)]
    [InlineData(TestSave.Endgame2, 76561197994931124UL)]
    public void ParseSteamID64(TestSave testSave, ulong steamId)
    {
      var file = testFiles[testSave];
      Assert.Equal(steamId, file.Header.SteamID);
      file.Header.SteamID = 10UL;
      Assert.Equal(10UL, file.Header.SteamID);
    }

    [Theory]
    [ClassData(typeof(AllTestSavesGenerator))]
    public void ValidateChecksum(TestSave testSave)
    {
      var file = testFiles[testSave];
      Assert.Equal(file.Header.DataHash.ToArray(), file.GenerateChecksum().ToArray());
    }

    [Theory]
    [ClassData(typeof(AllTestSavesGenerator))]
    public void ValidateLength(TestSave testSave)
    {
      var file = testFiles[testSave];
      Assert.Equal((int)file.Header.DataSize + 64, file.FileSize);
    }

    [Theory]
    [ClassData(typeof(AllTestSavesGenerator))]
    public void ParseSections(TestSave testSave)
    {
      var file = testFiles[testSave];
      var controls = file.Controls;
      var options = file.Options;
      var unknown = file.UnknownSection;
      var saves = file.SaveSlots;
    }
  }
}
