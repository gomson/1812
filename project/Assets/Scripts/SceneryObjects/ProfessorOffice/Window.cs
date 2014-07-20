﻿/*	This file is part of 1812: La aventura.

    1812: La aventura is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    1812: La aventura is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with 1812: La aventura.  If not, see <http://www.gnu.org/licenses/>.
*/

using UnityEngine;
using System.Collections;


public class Window : InteractiveObject {
	
	public bool isWindowsClosed = true;
	private Vector2 childPosition;

	protected override void InitializeInformation(){
		//Write here the info for your interactive object
		currentType = interactiveTypes.interactiveButNotPickable;

		definitionText.Add("Es una ventana");

		interactionText.Add("Voy a");
		interactionText.Add("abrirla");

	}

	void Update(){
		/*
		childPosition = this.GetComponentInChildren<Transform>().position;
		this.GetComponentInChildren<DebuggingWalkingPoint>().Called();
		if(isWindowsClosed){
			this.GetComponent<SpriteRenderer>().enabled = true;
		}
		else{
			this.GetComponent<SpriteRenderer>().enabled = false;
		}
		*/
	}

	public override void Mechanism(){
		if(isInteractiveObjectMechanismActivated){
			if(isWindowsClosed){
				this.GetComponent<SpriteRenderer>().enabled = false;
				interactionText.Clear();
				interactionText.Add("Voy a");
				interactionText.Add("cerrarla");
				isWindowsClosed = false;
			}
			else{
				this.GetComponent<SpriteRenderer>().enabled = true;
				interactionText.Clear();
				interactionText.Add("Voy a");
				interactionText.Add("abrirla");
				isWindowsClosed = true;
			}
			isInteractiveObjectMechanismActivated = false;
		}

	}
}