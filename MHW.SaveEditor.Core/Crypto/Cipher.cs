using System;
using System.Text;
using static MHW.SaveEditor.Core.Crypto.Utility;

namespace MHW.SaveEditor.Core.Crypto
{
  class Cipher
  {
    private readonly Blowfish blowfish;
    private readonly string key;

    public Cipher(string key)
    {
      this.key = key;
      blowfish = new Blowfish(Encoding.Default.GetBytes(this.key));
    }

    public Span<byte> Encipher(ReadOnlySpan<byte> data)
    { 
      return FlipEndian(blowfish.Encrypt_ECB(FlipEndian(data).ToArray()));
    }

    public Span<byte> Decipher(ReadOnlySpan<byte> data)
    {
      return FlipEndian(blowfish.Decrypt_ECB(FlipEndian(data).ToArray()));
    }
  }
}
