using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework
{
    public class Container : MonoBehaviour {
        private List<ContainerItem> _items;

        private class ContainerItem {
            public readonly Guid Id = Guid.NewGuid();
            public string Name;
            public int Maximum;

            private int _amountTaken = 0;

            public int AmountLeft => Maximum - _amountTaken;

            public int Get(int value) {
                if(_amountTaken + value > Maximum) {
                    int tooMuch = _amountTaken + value - Maximum;
                    _amountTaken = Maximum;
                    return value - tooMuch;
                }

                _amountTaken += value;
                return value;
            }


            public void Set(int value) {
                Maximum += value;
            }
        }

        private void Awake() {
            _items = new List<ContainerItem>();
        }

        public Guid Add(string itemName, int max) {
            _items.Add(new ContainerItem {
                Maximum = max,
                Name = itemName
            });

            return _items.Last().Id;
        }

        public void Put(string itemName, int value) {
            var containerItem = _items.FirstOrDefault(x => itemName == x.Name);

            containerItem?.Set(value);

        }

        public int TakeFromContainer(Guid id, int amount) {
            var containerItem = _items.FirstOrDefault(x => id == x.Id);
            if(containerItem == null) {
                return -1;
            }

            return containerItem.Get(amount);
        }

        public int LeftInInventory(Guid itemId) {
            var containerItem = _items.FirstOrDefault(x => x.Id == itemId);
            if(containerItem == null) {
                return 0;
            }

            return containerItem.AmountLeft;

        }
    }
}
