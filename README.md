# CodingTestEdwinMoreno

## How to Run

1. Clone the repository:
    ```bash
    git clone <repository-url>
    cd CodingTestEdwinMoreno
    ```

2. Open the solution in Visual Studio 2022.

3. Build and run the application:
    - Press `Ctrl+F5` to build and run without debugging.

4. Access the API endpoint:
    ```
    https://localhost:7253/api/stories?count=n
    ```

## Assumptions

- The default count of stories retrieved is 10 if its not specified.
- The cache duration for story IDs is set to 8 minutes to reduce load on the Hacker News API.

## Enhancements
- Add unit tests 
- Implement rate limiting 
