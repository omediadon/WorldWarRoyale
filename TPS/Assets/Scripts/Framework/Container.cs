using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Container: MonoBehaviour {
	private List<ContainerItem> items;

	private class ContainerItem {
		public Guid Id;
		public string Name;
		public int Maximum;

		private int amountTaken;

		public ContainerItem() {
			Id = Guid.NewGuid();
		}

		public int AmountLeft {
			get {
				return Maximum - amountTaken;
			}
		}

		public int Get(int value) {
			if(amountTaken + value > Maximum) {
				int tooMuch = amountTaken + value - Maximum;
				amountTaken = Maximum;
				return value - tooMuch;
			}

			amountTaken += value;
			return value;
		}

		public int Put(int value) {
			return -1;
		}
	}

	private void Awake() {
		items = new List<ContainerItem>();
	}

	public System.Guid Add(string name, int max) {
		items.Add(new ContainerItem {
			Maximum = max,
			Name = name
		});
		return items.Last().Id;
	}

	public int TakeFromContainer(Guid id, int amount) {
		var containerItem = items.Where(x =>  id == x.Id).FirstOrDefault();

		if(containerItem == null) {
			return -1;
		}

		return containerItem.Get(amount);
	}
}
