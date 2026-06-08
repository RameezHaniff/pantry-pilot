export interface OptimizationResponse{
    recipes: RecipeResult[]
    maxPeopleFed: number;
    unusedIngredients: UnusedIngredients[];
}

export interface RecipeResult {
    name: string;
    quantity: number;
    ingredients: RecipeIngredientResult[];
}

export interface RecipeIngredientResult {
    name: string;
    quantityPerServing: number;
    totalUsed: number
}

export interface UnusedIngredients{
    name: string;
    quantity: number;
}