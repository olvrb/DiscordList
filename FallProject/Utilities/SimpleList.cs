using System.Collections.Generic;
using System.Linq;

namespace FallProject.Utilities {
    public class SimpleList {
        private string _listAsStrings;

        public SimpleList(List<string> input) {
            SetList(input);
        }

        private List<string> SetList(List<string> input) {
            _listAsStrings = input
                             .Select(Base64Utilities.Base64Encode)
                             // Not sure why I'm not using Join here.
                             .Aggregate((x, y) => $"{x},{y},")
                             .Trim();
            return GetList();
        }

        // LINQ is beautiful.
        public List<string> GetList() => _listAsStrings
                                         .Split(",")
                                         // Remove empty indexes.
                                         .Where(x => !string.IsNullOrEmpty(x))
                                         // Decode each index from base64.
                                         .Select(Base64Utilities.Base64Decode)
                                         .ToList();

        public List<string> Add(string item) {
            _listAsStrings += $"{item.Base64Encode()},";
            return GetList();
        }

        public List<string> Remove(string item) {
            List<string> tempList = GetList();

            // It really does not matter if the method fails to remove the item.
            tempList.Remove(item);
            return SetList(tempList);
        }
    }
}