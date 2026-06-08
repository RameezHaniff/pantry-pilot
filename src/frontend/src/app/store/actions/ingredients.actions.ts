import { HttpErrorResponse } from "@angular/common/http"
import { IngredientResponse } from "../../models/ingredient-response"

let typeName = "Ingredients"

export namespace IngredientsActions {

    export class GetIngredients {
        static readonly type = `[${typeName}] ${this.name}`
        constructor() { }
    }

    export class GetIngredientsSuccess {
        static readonly type = `[${typeName}] ${this.name}`
        constructor(public payload: IngredientResponse[]) { }
    }

    export class GetIngredientsFailure {
        static readonly type = `[${typeName}] ${this.name}`
        constructor(public payload: HttpErrorResponse) { }
    }
}