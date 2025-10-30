using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.StackOverFlow.ValueObjects
{
    public sealed class TagVO : ValueObject<TagVO>
    {
        public TagVO(string name, long count)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; init; }
        public long Count { get; init; }


        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
            yield return Count;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Count);

        public override string ToString() => string.Join(", ", GetEqualityComponents().Select(c => c?.ToString() ?? ""));
    }
}
