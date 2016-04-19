using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace DpfsLinkTestApplication.__Console__ {

    [Serializable,
    ComVisible(true),
    ProgId("SimpleDictionary"),
    Guid("cea59369-9557-476e-8b9c-386f3fa5247e")]
    public class SimpleDictionary : IDictionary<string, bool> {


        private readonly IDictionary<string, bool> InnerDictionary;

        public SimpleDictionary() {
            this.InnerDictionary = new Dictionary<string, bool>();
        }

        public SimpleDictionary(int capacity) {
            this.InnerDictionary = new Dictionary<string, bool>(capacity);
        }

        public SimpleDictionary(IDictionary<string, bool> dictionary) {
            this.InnerDictionary = new Dictionary<string, bool>(dictionary);
        }

        #region Members of IDictionary<string, string>

        public void Add(string key, bool value) {
            this.InnerDictionary.Add(key, value);
        }

        public bool ContainsKey(string key) {
            return this.InnerDictionary.ContainsKey(key);
        }

        public ICollection<string> Keys {
            get {
                return this.InnerDictionary.Keys;
            }
        }

        public bool Remove(string key) {
            return this.InnerDictionary.Remove(key);
        }

        public bool TryGetValue(string key, out bool value) {
            return this.InnerDictionary.TryGetValue(key, out value);
        }

        public ICollection<bool> Values {
            get {
                return this.InnerDictionary.Values;
            }
        }

        public bool this[string key] {
            get {
                return this.InnerDictionary[key];
            }

            set {
                this.InnerDictionary[key] = value;
            }
        }

        #endregion

        #region Members of ICollection<KeyValuePair<string, bool>>

        public void Add(KeyValuePair<string, bool> item) {
            this.InnerDictionary.Add(item);
        }

        public void Clear() {
            this.InnerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, bool> item) {
            return this.InnerDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, bool>[] array, int arrayIndex) {
            this.InnerDictionary.CopyTo(array, arrayIndex);
        }

        public int Count {
            get {
                return this.InnerDictionary.Count;
            }
        }

        public bool IsReadOnly {
            get {
                return this.InnerDictionary.IsReadOnly;
            }
        }

        public bool Remove(KeyValuePair<string, bool> item) {
            return this.InnerDictionary.Remove(item);
        }

        #endregion

        #region Members of IEnumerable<KeyValuePair<string, bool>>

        public IEnumerator<KeyValuePair<string, bool>> GetEnumerator() {
            return this.InnerDictionary.GetEnumerator();
        }

        #endregion

        #region Members of IEnumerable

        IEnumerator IEnumerable.GetEnumerator() {
            return this.InnerDictionary.GetEnumerator();

        }
        #endregion

    }
}