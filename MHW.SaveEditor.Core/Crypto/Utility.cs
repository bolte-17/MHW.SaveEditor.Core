using System;

namespace MHW.SaveEditor.Core.Crypto
{
  public static class Utility
  {
    public static Span<byte> FlipEndian(ReadOnlySpan<byte> data)
    {
      var result = data.ToArray().AsSpan();
      for (var i = 0; i < data.Length; i += 4)
      {
        result[i..(i + 4)].Reverse();
      }
      return result;
    }
  }
}
