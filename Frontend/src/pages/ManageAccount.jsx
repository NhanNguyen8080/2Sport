import React from "react";
import { useForm } from 'react-hook-form';
import { Button, Input } from '@material-tailwind/react';
import UserProfile from "../components/User/UserProfile";

function ManageAccount() {
    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const onSubmit = data => console.log(data);
  
    return (
      <div className="flex flex-col md:flex-row">
        <nav className="md:w-1/4 p-4 bg-gray-100">
          <ul className="space-y-4">
            <li className="text-orange-500 font-semibold">My Profile</li>
            <li className="text-gray-500">Address Book</li>
            <li className="text-gray-500">My Payment Options</li>
            <li className="text-gray-500">My Returns</li>
            <li className="text-gray-500">My Cancellations</li>
            <li className="text-gray-500">My Wishlist</li>
          </ul>
        </nav>
        <div className="md:w-3/4 p-8">
          {/* <h2 className="text-orange-500 font-semibold text-xl mb-4">Edit Your Profile</h2>
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <Input
                type="text"
                label="Fullname"
                {...register('fullname', { required: true })}
                error={errors.fullname ? true : false}
              />
              <Input
                type="text"
                label="Gender"
                {...register('gender', { required: true })}
                error={errors.gender ? true : false}
              />
            </div>
            <Input
              type="email"
              label="Email"
              {...register('email', { required: true })}
              error={errors.email ? true : false}
            />
            <Input
              type="text"
              label="Address"
              {...register('address', { required: true })}
              error={errors.address ? true : false}
            />
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <Input
                type="password"
                label="Current Password"
                {...register('currentPassword', { required: true })}
                error={errors.currentPassword ? true : false}
              />
              <Input
                type="password"
                label="New Password"
                {...register('newPassword', { required: true })}
                error={errors.newPassword ? true : false}
              />
              <Input
                type="password"
                label="Confirm New Password"
                {...register('confirmNewPassword', { required: true })}
                error={errors.confirmNewPassword ? true : false}
              />
            </div>
            <div className="flex justify-end space-x-4">
              <Button
                color="red"
                variant="outlined"
                type="button"
              >
                Cancel
              </Button>
              <Button
                color="orange"
                variant="filled"
                type="submit"
              >
                Save Changes
              </Button>
            </div>
          </form> */}
          <UserProfile/>
        </div>
      </div>
    );
  };

export default ManageAccount;
