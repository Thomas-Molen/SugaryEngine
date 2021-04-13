@extends('layouts.app')
@section('title', 'Tickets')
@section('content')
    <!-- Content Header (Page header) -->
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0 text-dark">Leaderboard</h1>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-bordered table-striped SpeedRunnerTable">
                                    <thead>
                                    <tr>
                                        <th>Rank</th>
                                        <th>Title</th>
                                        <th>Player</th>
                                        <th>Time (h:m:s:ms)</th>
                                        <th>Date (y-d-m UTC)</th>
                                        <th>Actions</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    @foreach ( (new App\Http\Controllers\LeaderboardController)->SortRuns() as $run)
                                        @if ($run->active === 1)
                                            <tr>
                                                @if ($i === 0)
                                                    <td><i class="fas fa-trophy"></i>{{ ++$i }}</td>
                                                @elseif ($i === 1)
                                                    <td><i class="fas fa-award"></i>{{ ++$i }}</td>
                                                @elseif ($i === 2)
                                                    <td><i class="fas fa-medal"></i>{{ ++$i }}</td>
                                                @else
                                                    <td>{{ ++$i }}</td>
                                                @endif
                                                <td>{{ $run->custom_name}}</td>
                                                <td>{{ (new App\Http\Controllers\RunController)->GetUsername($run->user_id) }}</td>

                                                <td>{{$time = (new App\Http\Controllers\LeaderboardController)->FormatTime($run->duration)}}</td>
                                                <td>{{ $run->created_at}}</td>
                                                <td><form action="{{ route('run.destroy',$run->id) }}" method="POST">
                                                        <a class="btn btn-sm btn-primary " href="{{ route('run.show',$run->id) }}"><i class="fa fa-fw fa-eye"></i> Show</a>
                                                        @if (auth()->user())
                                                            <a class="btn btn-sm btn-success" href="{{ route('user_create_comment', $run->id) }}"><i class="nav-icon fas fa-comments"></i> Comment</a>
                                                        @endif
                                                        @if($run->user_id === auth()->id())
                                                            <a class="btn btn-sm btn-secondary" href="{{ route('run.edit',$run->id) }}"><i class="fa fa-fw fa-edit"></i> Edit</a>
                                                            @csrf
                                                            @method('DELETE')
                                                            <button type="submit" class="btn btn-danger btn-sm"><i class="fa fa-fw fa-trash"></i> Delete</button>
                                                        @endif

                                                    </form></td>
                                            </tr>
                                            @endif
                                            @endforeach
                                    </tbody>
                                    <tfoot>
                                    <tr>
                                        <th>Rank</th>
                                        <th>Title</th>
                                        <th>Player</th>
                                        <th>Time (h:m:s:ms)</th>
                                        <th>Date (y-d-m UTC)</th>
                                        <th>Actions</th>
                                    </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
@endsection
