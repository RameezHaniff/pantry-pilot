import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environment/evironment';
import { OptimizationRequest } from '../models/optimization-request';
import { OptimizationResponse } from '../models/optimization-response';

@Injectable({
  providedIn: 'root'
})
export class FoodOptimizerGateway {

    private apiUrl = environment.apiUrl + "/foodOptimizer";

    constructor(private http: HttpClient) { }

    optimize(request: OptimizationRequest) {
        return this.http.post<OptimizationResponse>(this.apiUrl,request);
    }
}