using FoodHub.Fitness.Entities;
using FoodHub.Fitness.Enums;
using FoodHub.Fitness.Repositories;
using FoodHub.Fitness.Services;
using FoodHub.Fitness.Validation;

int passed = 0;

void Check(bool condition, string description)
{
    if (!condition) throw new Exception("FAIL: " + description);
    passed++;
    Console.WriteLine("PASS: " + description);
}

void Throws<TException>(Action action, string description) where TException : Exception
{
    try { action(); }
    catch (TException) { Check(true, description); return; }
    throw new Exception("FAIL: " + description);
}

// Entity relationships, keys, and validation.
var role = new Role(1, "Member");
var user = new User(1, "member@example.com", 25, 80m, 180m, Gender.Male, role.Id);
var workout = new Workout(1, "Strength", true);
var freeWorkout = new Workout(2, "Mobility", false);
var exercise = new Exercise(1, "Squat");
var join = new WorkoutExercise(1, workout.Id, exercise.Id, 4, 8);
var mealPlan = new MealPlan(1, user.Id, "Balanced", 2400);
var nutrition = new NutritionLog(1, user.Id, 520, 38m, 60m, 12m, mealPlan.Id);
var review = new Review(1, user.Id, workout.Id, 5, "Great workout");
var promo = new PromoCode(1, "FIT2026", 15m);
Check(join.WorkoutId == workout.Id && join.ExerciseId == exercise.Id && join.Sets == 4,
    "WorkoutExercise N:M join and foreign keys");
Check(mealPlan.UserId == user.Id && nutrition.MealPlanId == mealPlan.Id,
    "MealPlan and NutritionLog foreign keys");
Check(review.WorkoutId == workout.Id && review.UserId == user.Id && promo.Code == "FIT2026",
    "Review and PromoCode entities");

// Generic repository CRUD including not-found and duplicate protections.
IRepository<Exercise> repository = new InMemoryRepository<Exercise>();
repository.Add(exercise);
Check(repository.GetAll().Count() == 1 && repository.GetById(1)?.Name == "Squat",
    "Repository Add, GetAll, GetById");
repository.Update(new Exercise(1, "Deadlift"));
Check(repository.GetById(1)?.Name == "Deadlift", "Repository Update");
Throws<InvalidOperationException>(() => repository.Add(new Exercise(1, "Duplicate")),
    "Repository duplicate Id rejected");
repository.Delete(1);
Check(repository.GetById(1) is null && !repository.GetAll().Any(),
    "Repository Delete");
Throws<KeyNotFoundException>(() => repository.Delete(1),
    "Repository missing Id rejected");

// BMR, TDEE, target adjustment (Mifflin-St Jeor).
var calculator = new BmrCalculatorService();
Check(calculator.CalculateBmr(Gender.Male, 25, 80m, 180m) == 1805m,
    "Male BMR = 1805");
Check(calculator.CalculateBmr(Gender.Female, 25, 80m, 180m) == 1639m,
    "Female BMR = 1639");
Check(calculator.CalculateTdee(Gender.Male, 25, 80m, 180m, ActivityLevel.ModeratelyActive)
    == 2797.75m, "Moderately-active TDEE = 2797.75");
Check(calculator.CalculateDailyCalories(Gender.Male, 25, 80m, 180m,
    ActivityLevel.ModeratelyActive, FitnessGoal.LoseWeight) == 2378.09m,
    "Weight loss goal reduces calories by 15%");
Check(calculator.CalculateDailyCalories(Gender.Male, 25, 80m, 180m,
    ActivityLevel.Sedentary, FitnessGoal.GainMass) == 2490.90m,
    "Muscle gain goal increases calories by 15%");

// Premium access (only active subscriptions qualify).
var access = new WorkoutAccessService();
Check(access.CanUserAccessWorkout(user, freeWorkout), "Free workout is accessible");
Check(!access.CanUserAccessWorkout(user, workout), "Premium blocked without subscription");
var subscription = new Subscription(1, user.Id,
    DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(30), true, promo.Id);
user.AddSubscription(subscription);
Check(access.CanUserAccessWorkout(user, workout), "Active subscription unlocks Premium");
subscription.Deactivate();
Check(!access.CanUserAccessWorkout(user, workout), "Inactive subscription blocks Premium");
Throws<ArgumentException>(() => user.AddSubscription(
    new Subscription(2, 999, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(2), true)),
    "Cannot attach another user's subscription");

// Input validation.
Check(InputValidator.IsValidEmail("name@example.com") &&
    !InputValidator.IsValidEmail("not-an-email"), "Email validation");
Check(InputValidator.IsValidPromoCode("FIT2026") &&
    !InputValidator.IsValidPromoCode("fit-2026"), "Promo format validation");
Throws<ArgumentOutOfRangeException>(() =>
    calculator.CalculateBmr(Gender.Male, 0, 80m, 180m), "Age must be positive");
Throws<ArgumentOutOfRangeException>(() =>
    calculator.CalculateBmr(Gender.Male, 25, -1m, 180m), "Weight must be positive");
Throws<ArgumentOutOfRangeException>(() =>
    calculator.CalculateBmr(Gender.Male, 25, 80m, 0m), "Height must be positive");
Throws<ArgumentException>(() =>
    new User(2, "bad", 25, 80m, 180m, Gender.Male, 1), "User email validated");
Throws<ArgumentException>(() => new PromoCode(2, "invalid!", 10m),
    "PromoCode constructor validates format");

Console.WriteLine($"All {passed} smoke checks passed.");
