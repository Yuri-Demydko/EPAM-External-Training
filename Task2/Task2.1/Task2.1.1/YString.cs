#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YStrings
{
    public class YString:IComparable,IEnumerable
    {
        private char[] _chars;
        
        public YString(char[] chars)
        {
            this._chars = new char[chars.Length];
            chars.CopyTo(this._chars,0);
        }
        public YString(string str):this(str.ToCharArray())
        {
        }

        public YString(char c, int count) : this(Enumerable.Repeat<char>(c, count).ToArray())
        {
            
        }
        
        char this [int index]
        {
            get => _chars[index];
            set => _chars[index] = value;
        }
        private bool Equals(YString other) => _chars.SequenceEqual(other._chars);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((YString) obj);
        }

        public override int GetHashCode()
        {
            return _chars.GetHashCode();
        }
        
        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                YString ystr => ystr._chars.Length.CompareTo(this._chars.Length),
                string str => str.Length.CompareTo(this._chars.Length),
                char[] chrs => chrs.Length.CompareTo(this._chars.Length),
                _ => throw new ArgumentException("Object isn't string, YString or char[]!")
            };
        }
        
        public override string ToString() => new(_chars);
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
        private IEnumerator GetEnumerator()
        {
            return _chars.GetEnumerator();
        }

        public char[] ToCharArray()
        {
            var res = new char[this._chars.Length];
            this._chars.CopyTo(res,0);
            return res;
        }
        

        public int? IndexOf(char c)
        {
            for (var i = 0; i < _chars.Length; i++)
            {
                if (_chars[i] == c) return i;
            }

            return null;
        }
        public int? IndexOf(string substr)
        {
            for (var i = 0; i < _chars.Length; i++)
            {
                if (_chars[i] != substr[0]) continue;
                var found = !substr
                    .Where((t, j) => _chars[i + j] != t)
                    .Any();
                if (found) return i;
            }

            return null;
        }

        public int? IndexOf(YString ysubstr) => IndexOf(ysubstr.ToString());

        public int Length => _chars.Length;

        public static bool operator >(YString ystr1, YString ystr2) 
            => ystr1._chars.Length > ystr2._chars.Length;

        public static bool operator <(YString ystr1, YString ystr2)
            => ystr1._chars.Length < ystr2._chars.Length;
        
        public static bool operator >=(YString ystr1, YString ystr2) 
            => ystr1._chars.Length >= ystr2._chars.Length;

        public static bool operator <=(YString ystr1, YString ystr2)
            => ystr1._chars.Length <= ystr2._chars.Length;

        public static YString operator +(YString ystr1, YString ystr2)
            => new YString(ystr1._chars.Concat(ystr2._chars).ToArray());

        //Addition to .NET basic strings' functionality
        public static YString operator -(YString ystr1, YString ystr2)
        => new YString(ystr1._chars.Where(c => !ystr2._chars.Contains(c)).ToArray());
        

        public static bool operator ==(YString ystr1, YString ystr2)
            => ystr1._chars.SequenceEqual(ystr2._chars);

        public static bool operator !=(YString ystr1, YString ystr2) 
            => !(ystr1 == ystr2);
        
        //Additions to .NET basic strings' functionality
        public static YString FromCharCodesSequence(IEnumerable<byte> array)
        {
            return new YString(Encoding.Unicode.GetChars(array.ToArray()));
        }

        public static IEnumerable<byte> GetCharCodesSequence(YString ystr)
        {
            return Encoding.Unicode.GetBytes(ystr._chars);
        }

        public YString CapitalizeSentence()
        {
            var res = new StringBuilder();
            var splitters = new [] {"...", ".", "!", "?", "?!"};

            var split= this.ToString()
                .Split(' ', StringSplitOptions.TrimEntries);

            res.Append(split[0][0].ToString().ToUpper() + split[0][1..]).Append(' ');
            for (var i = 1; i < split.Length; i++)
            {
                if (splitters.Any(s => split[i - 1].Contains(s)))
                    res.Append(split[i][0].ToString().ToUpper() + split[i][1..]).Append(' ');
                else res.Append(split[i]).Append(' ');
            }

            return new YString(res.ToString());
        }
        
    }
}