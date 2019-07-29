using MHW.SaveEditor.Core;
using System.Collections.Generic;
using Xunit;

namespace MHW.SaveEditor.Test
{
  [Collection("Test Saves")]
  public class SaveSlotTests
  {
    private readonly IDictionary<TestSave, SaveFile> testSaves;
    private SaveSlot GetSaveSlot(TestSave testSave, int slotNumber) => testSaves[testSave].SaveSlots[slotNumber];
    public SaveSlotTests(TestSavesFixture fixture) => testSaves = fixture.TestSaves;

    [Theory]
    [InlineData(TestSave.NewGame, 0, "Jim")]
    [InlineData(TestSave.PostXeno, 0, "Rename Me")]
    [InlineData(TestSave.Endgame1, 0, "RenameMe")]
    [InlineData(TestSave.Endgame2, 0, "Raine")]
    public void ReadCharacterName(TestSave testSave, int slotNumber, string name) => Assert.Equal(name, GetSaveSlot(testSave, slotNumber).CharacterName);

    [Theory]
    [InlineData(TestSave.NewGame, 0, 1u)]
    [InlineData(TestSave.PostXeno, 0, 29u)]
    [InlineData(TestSave.Endgame1, 0, 100u)]
    [InlineData(TestSave.Endgame2, 0, 115u)]
    public void ReadHunterRank(TestSave testSave, int slotNumber, ulong rank) => Assert.Equal(rank, GetSaveSlot(testSave, slotNumber).HunterRank);

    [Theory]
    [InlineData(TestSave.NewGame, 0, 2000u)]
    [InlineData(TestSave.PostXeno, 0, 308033u)]
    [InlineData(TestSave.Endgame1, 0, 0u)]
    [InlineData(TestSave.Endgame2, 0, 1941471u)]
    public void ReadZeni(TestSave testSave, int slotNumber, ulong zeni) => Assert.Equal(zeni, GetSaveSlot(testSave, slotNumber).Zeni);

    [Theory]
    [InlineData(TestSave.NewGame, 0, 0u)]
    [InlineData(TestSave.PostXeno, 0, 20214u)]
    [InlineData(TestSave.Endgame1, 0, 0u)]
    [InlineData(TestSave.Endgame2, 0, 106699u)]
    public void ReadResearchPoints(TestSave testSave, int slotNumber, ulong rp) => Assert.Equal(rp, GetSaveSlot(testSave, slotNumber).ResearchPoints);

    [Theory]
    [InlineData(TestSave.NewGame, 0, 0u)]
    [InlineData(TestSave.PostXeno, 0, 52510u)]
    [InlineData(TestSave.Endgame1, 0, 281300u)]
    [InlineData(TestSave.Endgame2, 0, 341984u)]
    public void ReadHunterXP(TestSave testSave, int slotNumber, ulong xp) => Assert.Equal(xp, GetSaveSlot(testSave, slotNumber).HunterXP);

    [Theory]
    [InlineData(TestSave.NewGame, 0, 1540u)]
    [InlineData(TestSave.PostXeno, 0, 146584u)]
    [InlineData(TestSave.Endgame1, 0, 0u)]
    [InlineData(TestSave.Endgame2, 0, 459110u)]
    public void ReadPlayTime(TestSave testSave, int slotNumber, ulong playtime) => Assert.Equal(playtime, GetSaveSlot(testSave, slotNumber).Playtime);
  }
}
