/* ----------------------------------------

	/*
	 * Ideal Forms 1.02
	 * Copyright 2011, Cedric Ruiz
	 * Free to use under the GPL license.
	 * http://www.spacirdesigns.com

	 
	* Support for:
	* IE8+
	* Firefox 3.6+
	* Chrome 9+
	* Safari 4+
	* Opera 11+
	 
-----------------------------------------*/

NOTES

* Requires jQuery 1.7+
* Code re-written from scracth.
* Using LESS, the dynamic stylesheet language, instead of vanilla css.
* Much easier to make custom themes. Open "idealforms.less" and check it out!
* Now you can retrieve data from any element using regular php and the name attribute; no need to use js/ajax.
* Select menu scrollbar fixed and working in all browsers.
* Keyboard actions are NOT supported yet.
* New syntax (check the index.html example):
	
	<form>
	
		<div> /* Text input */
			<label class="required">Name:</label>
			<input type="text" />
		</div>
		
		<div> /* Select menu */
			<label>Options:</label>
			<select>
				<option>Number 1</option>
				<option>Number 2</option>
				<option>Number 3</option>
			</select>
		</div>		
		
		<div> /* Radios */
			<label>Radios:</label>
			<ul>
				<li><label><input type="radio" name="radios" checked />Option 1</label></li>
				<li><label><input type="radio" name="radios" />Option 2</label></li>
				<li><label><input type="radio" name="radios" />Option 3</label></li>
			</ul>
		</div>			
		
		<div> /* Checkboxes */
			<label>Radios:</label>
			<ul>
				<li><label><input type="checkbox" name="checkboxes" />Option 1</label></li>
				<li><label><input type="checkbox" name="checkboxes"/>Option 2</label></li>
				<li><label><input type="checkbox" name="checkboxes" />Option 3</label></li>
			</ul>
		</div>

		<div> /* Buttons */
			<label>&nbsp;</label> /* Insert empty label to align buttons to the rest of elements */
			<button></button>
			<input type="submit" />
			<input type="reset" />
		</div>		
		
	</form>