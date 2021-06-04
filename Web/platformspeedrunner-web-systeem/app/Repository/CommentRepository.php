<?php


namespace App\Repository;


use App\Models\Comment;
use Illuminate\Http\Request;

class CommentRepository
{
    private $repository;

    public function __construct(Repository $queryHelper)
    {
        $this->repository = $queryHelper;
    }

    public function GetUserComment()
    {
        return Comment::where('active', '=', true)->where('user_id', '=', auth()->user()->id)->get();
    }

    public function GetRunComment($run_id)
    {
        return Comment::where('active', '=', true)->where('run_id', '=', $run_id)->get();
    }
}
