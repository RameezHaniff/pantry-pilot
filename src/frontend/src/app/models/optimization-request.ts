export interface OptimizationRequest {
    ingredients: IngredientQuantity[];
}

export interface IngredientQuantity {
    ingredientId: number;
    quantity: number;
}