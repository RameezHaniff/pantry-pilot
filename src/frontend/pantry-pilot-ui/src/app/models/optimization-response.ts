export interface OptimizationResponse{
    recipes: RecipeResult[]
    maxPeopleFed: number;
}

export interface RecipeResult {
    name: string;
    quantity: number;
}