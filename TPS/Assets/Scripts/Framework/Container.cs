﻿using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Container : MonoBehaviour {
	List<ContainerItem> items;

	private class ContainerItem {
		public Guid Id;
		public string Name;
		public int Maximum;

		private int amountTaken = 0;

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


		public void Set(int value) {
			Maximum += value;
		}
	}

	private void Awake() {
		items = new List<ContainerItem>();
	}

	public Guid Add(string name, int max) {
		items.Add(new ContainerItem {
			Maximum = max,
			Name = name
		});

		return items.Last().Id;
	}

	public void Put(string name, int value) {
		var containerItem = items.Where(x =>  name == x.Name).FirstOrDefault();
		if(containerItem == null) {
			return;
		}

		containerItem.Set(value);

	}

	public int TakeFromContainer(Guid id, int amount) {
		var containerItem = items.Where(x =>  id == x.Id).FirstOrDefault();
		if(containerItem == null) {
			return -1;
		}

		return containerItem.Get(amount);
	}

	public int LeftInInventory(Guid itemId) {
		var containerItem = items.Where(x => x.Id == itemId).FirstOrDefault();
		if(containerItem == null) {
			return 0;
		}

		return containerItem.AmountLeft;

	}
}
