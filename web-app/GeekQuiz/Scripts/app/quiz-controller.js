angular.module('QuizApp', [])
    .controller('QuizCtrl', function ($scope, $http) {
        $scope.answered = false;
        $scope.title = "loading question...";
        $scope.options = [];
        $scope.correctAnswer = false;
        $scope.working = false;

        $scope.answer = function () {
            return $scope.correctAnswer ? 'correct' : 'incorrect';
        };

        $scope.nextQuestion = function () {
            $scope.working = true;
            $scope.answered = false;
            $scope.title = "loading question...";
            $scope.options = [];

            $http.get("/api/trivia").success(function (data, status, headers, config) {
                $scope.options = data.Options;
                $scope.title = data.Title;
                $scope.answered = false;
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        };

        $scope.sendAnswer = function (option) {
            $scope.working = true;
            $scope.answered = true;

            $http.post('/api/trivia', { 'QuestionId': option.QuestionId, 'OptionId': option.Id }).success(function (data, status, headers, config) {
                $scope.correctAnswer = (data === true);
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Oops... something went wrong";
                $scope.working = false;
            });
        };
    });