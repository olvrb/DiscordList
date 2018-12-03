using System.Collections.Generic;
using System.Linq;

namespace FallProject.Utilities {
    public class SimpleList {
        private string ListAsStrings;

        private List<string> SetList(List<string> input) {
            ListAsStrings = input.Select(Base64Utilities.Base64Encode).Aggregate((x, y) => $"{x},{y},");
            return GetList();
        }

        public List<string> GetList() => ListAsStrings.Split(",").Select(Base64Utilities.Base64Decode).ToList();

        public SimpleList(List<string> input) {
            SetList(input);
        }

        public List<string> Add(string item) {
            ListAsStrings += $"{item.Base64Encode()},";
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