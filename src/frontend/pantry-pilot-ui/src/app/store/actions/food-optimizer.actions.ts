import { HttpErrorResponse } from "@angular/common/http"
import { OptimizationRequest } from "../../models/optimization-request"
import { OptimizationResponse } from "../../models/optimization-response"

let typeName = "FoodOptimizer"

export namespace FoodOptimizerActions {

    export class Optimize {
        static readonly type = `[${typeName}] ${this.name}`
        constructor(public payload: OptimizationRequest) { }
    }

    export class OptimizeSuccess {
        static readonly type = `[${typeName}] ${this.name}`
        constructor(public payload: OptimizationResponse) { }
    }

    export class OptimizeFailure {
        static readonly type = `[${typeName}] ${this.name}`
        constructor(public payload: HttpErrorResponse) { }
    }

    export class ResetOptimizationResult {
        static readonly type = `[${typeName}] ${this.name}`
        constructor() { }
    }
}