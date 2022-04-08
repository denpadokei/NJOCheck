using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NJOCheck.Extentions
{
    public static class NoteJumpStartBeatOffsetDropdownExtention
    {
        private static readonly ReadOnlyDictionary<float, int> dorpDownId;
        static NoteJumpStartBeatOffsetDropdownExtention()
        {
            var dic = new Dictionary<float, int>();
            foreach (var item in GetNamedValues().Select((x, i) => (x, i))) {
                dic.Add(item.x, item.i);
            }
            dorpDownId = new ReadOnlyDictionary<float, int>(dic);
        }

        public static int GetIdxForOffset(this NoteJumpStartBeatOffsetDropdown dropdown, float @value)
        {
            if (dorpDownId.TryGetValue(value, out var id)) {
                return id;
            }
            else {
                return 0;
            }
        }

        public static IReadOnlyList<float> GetNamedValues()
        {
            return new List<float>
            {
                -0.5f,
                -0.25f,
                0f,
                0.25f,
                0.5f,
            };
        }
    }
}
