<?php

namespace App\Http\Controllers;

use App\Models\Comment;
use App\Models\User;
use App\Models\Run;
use Illuminate\Http\Request;

/**
 * Class CommentController
 * @package App\Http\Controllers
 */
class CommentController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        if ((new AuthenticatorController)->AuthAccess()) {
            $comments = Comment::paginate();

            return view('comment.index', compact('comments'))
                ->with('i', (request()->input('page', 1) - 1) * $comments->perPage());
        }
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        if (auth()->user()) {
            $comment = new Comment();
            return view('comment.create', compact('comment'));
        }
    }

    /**
     * Show the form for creating a new resource as a normal user.
     *
     * @return \Illuminate\Http\Response
     */
    public function user_create(Request $request, int $id)
    {
        $comment = new Comment();

        return view('comment.user_create')->with(['comment' => $comment, 'run_id' => $id]);
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        request()->validate(Comment::$rules);
        $comment = Comment::create($request->all());
        if (empty($request->user_id))
        {
            $comment->update(['user_id' => auth()->id()]);
        }
        if (empty($request->run_id))
        {
            $comment->update(['run_id' => $request->run_id]);
        }
        $comment->update(['created_at' => date('Y-m-d H:i:s')]);


        return redirect()->route('comment.index')
            ->with('success', 'Comment created successfully.');
    }

    /**
     * Display the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function show($id)
    {
        if ((new AuthenticatorController)->AuthAccess()) {
            $comment = Comment::find($id);

            return view('comment.show', compact('comment'));
        }
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        if ((new AuthenticatorController)->IsCurrentUser($id)) {
            $comment = Comment::find($id);

            return view('comment.edit', compact('comment'));
        }
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request $request
     * @param  Comment $comment
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, Comment $comment)
    {
        request()->validate(Comment::$rules);

        $comment->update($request->all());

        return redirect()->route('comment.index')
            ->with('success', 'Comment updated successfully');
    }

    /**
     * @param int $id
     * @return \Illuminate\Http\RedirectResponse
     * @throws \Exception
     */
    public function destroy($id)
    {
        $comment = Comment::find($id)->update(['active' => 0]);

        return redirect()->route('comment.index')
            ->with('success', 'Comment deleted successfully');
    }
}
