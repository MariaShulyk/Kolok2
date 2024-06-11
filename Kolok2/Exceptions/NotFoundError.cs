namespace Kolok2.Exceptions;

public class NotFoundError(string message) : Exception(message);
public class BadRequestError(string message) : Exception(message);