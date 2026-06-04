import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/evironment';
import { IngredientResponse } from '../models/ingredient-response';

@Injectable({
  providedIn: 'root'
})
export class IngredientsGateway {

    private apiUrl = environment.apiUrl + "/ingredients";

    constructor(private http: HttpClient) { }

    getIngredients() {
        return this.http.get<IngredientResponse[]>(this.apiUrl);
    }
}