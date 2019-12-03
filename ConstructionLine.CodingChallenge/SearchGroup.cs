using System;

namespace ConstructionLine.CodingChallenge
{
    public class SearchGroup : IEquatable<SearchGroup>
    {
        public SearchGroup(Size size, Color color)
        {
            Size = size;
            Color = color;
        }

        public Size Size { get; }
        public Color Color { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as SearchGroup);
        }

        public bool Equals(SearchGroup other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Color?.Id == Color?.Id
                && other.Size?.Id == Size?.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 23) + (Color == null ? 0 : Color.Id.GetHashCode());
                hashCode = (hashCode * 23) + (Size == null ? 0 : Size.Id.GetHashCode());
                
                return hashCode;
            }
        }
    }
}