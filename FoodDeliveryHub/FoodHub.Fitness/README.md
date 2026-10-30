# FoodHub Fitness — Sprint 1 (C# / .NET 8)

This C# class library is added to the existing FoodHub Visual Studio solution.
The original food-delivery MVC host is an F# project (`FoodDeliveryHub.fsproj`).
We leave that project untouched rather than silently replacing it.

## Folder structure
- `Entities/`: User, Role, Workout, Exercise, WorkoutExercise, MealPlan,
  NutritionLog, Subscription, Review, PromoCode, plus IEntity and Entity
- `Enums/`: Gender, ActivityLevel, FitnessGoal
- `Validation/InputValidator.cs`: positive measurements, email, promo codes
- `Repositories/IRepository.cs`: GetAll, GetById, Add, Update, Delete
- `Repositories/InMemoryRepository.cs`: working in-memory CRUD implementation
- `Services/BmrCalculatorService.cs`: Mifflin-St Jeor BMR, TDEE, goal adjustment
- `Services/WorkoutAccessService.cs`: free/premium workout access

## How to run in Visual Studio 2022
1. Check out the `feature/fitness-sprint1` branch.
2. Open `FoodDeliveryHub/FoodDeliveryHub.sln`.
3. Make sure the .NET 8 SDK and C# workload are installed.
4. In Solution Explorer, select `FoodHub.Fitness.SmokeTests` and
   **Set as Startup Project**; then run with Ctrl+F5.
5. Alternatively, from the repository root:
   `dotnet run --project FoodDeliveryHub/FoodHub.Fitness.SmokeTests`

## Design decisions and limits
- Ten fitness entities are intentionally kept separate from existing
  food-delivery models, including the other User, Role, and Review classes.
- [Key] and [ForeignKey] attributes declare model keys; no EF Core DbContext or
  database migrations are included in this sprint.
- WorkoutExercise uses its own Id as primary key and WorkoutId/ExerciseId as
  foreign keys. A later database implementation should enforce a unique
  constraint on that pair.
- InMemoryRepository is functional CRUD but **not persistent**. Data vanishes
  when the process stops. This matches the repository-layer demonstration but
  is not a replacement for a database.
- GetById returns null if absent; Update/Delete throw KeyNotFoundException.
- Subscription access follows the assignment literally: only IsActive is
  checked. Expiration dates are stored but not used to gate access.
- Promo-code format is a project rule: 6–12 uppercase Latin letters/digits.
- Activity multipliers used for TDEE: 1.2, 1.375, 1.55, 1.725, 1.9.
- Goal multipliers: losing 0.85, maintaining 1.0, gaining 1.15.
- The demo checks are lightweight smoke tests, not a full test suite.
